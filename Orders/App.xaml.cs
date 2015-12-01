using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Orders
{
    public sealed partial class App : Application
    {
        private TransitionCollection transitions;
        
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
        }
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;
            
            if (rootFrame == null)
            {
                // Crear un marco para que actúe como contexto de navegación y navegar a la primera página.
                rootFrame = new Frame();

                // TODO: Cambiar este valor a un tamaño de caché adecuado para la aplicación
                rootFrame.CacheSize = 1;

                // Establecer el idioma predeterminado
                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Cargar el estado de la aplicación suspendida previamente
                }

                // Poner el marco en la ventana actual.
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // Quita la navegación de transición en el inicio.
                if (rootFrame.ContentTransitions != null)
                {
                    this.transitions = new TransitionCollection();
                    foreach (var c in rootFrame.ContentTransitions)
                    {
                        this.transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += this.RootFrame_FirstNavigated;

                // Cuando no se restaura la pila de navegación, navegar a la primera página,
                // configurando la nueva página pasándole la información requerida como
                //parámetro de navegación
                if (!rootFrame.Navigate(typeof(MainPage), e.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            // Asegurarse de que la ventana actual está activa.
            Window.Current.Activate();
        }

        /// <summary>
        /// Restaura las transiciones de contenido después de iniciar la aplicación.
        /// </summary>
        /// <param name="sender">Objeto al que se asocia el controlador.</param>
        /// <param name="e">Detalles sobre el evento de navegación.</param>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }

        /// <summary>
        /// Se invoca al suspender la ejecución de la aplicación.  El estado de la aplicación se guarda
        /// sin saber si la aplicación se terminará o se reanudará con el contenido
        /// de la memoria aún intacto.
        /// </summary>
        /// <param name="sender">Origen de la solicitud de suspensión.</param>
        /// <param name="e">Detalles sobre la solicitud de suspensión.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Guardar el estado de la aplicación y detener toda actividad en segundo plano
            deferral.Complete();
        }
    }
}