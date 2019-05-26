using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using LaUI.AsyncUISupport;

namespace LaUI.Controls
{
    public class AsyncBox:FrameworkElement
    {
        private Type _childType;
        private readonly HostVisual _hostVisual;
        private VisualTargetPresentationSource _targetSource;

        public FrameworkElement Child { get; private set; }
        public Type ChildType
        {
            get => _childType;
            set
            {
                if (_childType == value) return;
                if (value == null) return;

                CreateChildAsync(value).ContinueWith((Task<FrameworkElement> t) =>
                {
                    Child = t.Result;
                }, TaskContinuationOptions.None);

                _childType = value;
            }
        }


        public AsyncBox()
        {
            _hostVisual = new HostVisual();
        }

        protected override int VisualChildrenCount => _hostVisual == null ? 0 : 1;

        protected override Visual GetVisualChild(int index) => _hostVisual;

        protected override Size MeasureOverride(Size availableSize)
        {
            if (Child == null) return default(Size);

            Child.Dispatcher.InvokeAsync(() =>
            {
                Child.Measure(availableSize);
                return Child.DesiredSize;
            }, DispatcherPriority.Loaded);
            return default(Size);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Child == null) return finalSize;

            Child.Dispatcher.InvokeAsync(() =>
            {
                Child.Arrange(new Rect(finalSize));
            }, DispatcherPriority.Loaded);

            return finalSize;
        }

        private async Task<FrameworkElement> CreateChildAsync(Type childType)
        {
            if (Child != null)
            {
                _targetSource.Dispose();
            }
            Dispatcher dispatcher = await CreateDispatcherAsync();
            TaskCompletionSource<FrameworkElement> tc = new TaskCompletionSource<FrameworkElement>();
            await dispatcher.InvokeAsync(() =>
            {
                FrameworkElement element = Activator.CreateInstance(childType) as FrameworkElement;
                _targetSource = new VisualTargetPresentationSource(_hostVisual)
                {
                    RootVisual = element
                };
                tc.SetResult(element);
            });
            AddVisualChild(_hostVisual);
            InvalidateMeasure();
            return await tc.Task;
        }

        public Task<Dispatcher> CreateDispatcherAsync()
        {
            TaskCompletionSource<Dispatcher> tc = new TaskCompletionSource<Dispatcher>();
            Thread thread = new Thread(() =>
            {
                Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
                tc.SetResult(dispatcher);
                SynchronizationContext.SetSynchronizationContext(
                    new DispatcherSynchronizationContext(dispatcher));
                Dispatcher.Run();
            })
            {
                IsBackground = true,
                Name = "BackgroundUIThread"
            };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tc.Task;
        }
    }
}
