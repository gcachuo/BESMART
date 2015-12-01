using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=391641

namespace Orders
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Se invoca cuando esta página se va a mostrar en un objeto Frame.
        /// </summary>
        /// <param name="e">Datos de evento que describen cómo se llegó a esta página.
        /// Este parámetro se usa normalmente para configurar la página.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Preparar la página que se va a mostrar aquí.

            // TODO: Si la aplicación contiene varias páginas, asegúrese de
            // controlar el botón para retroceder del hardware registrándose en el
            // evento Windows.Phone.UI.Input.HardwareButtons.BackPressed.
            // Si usa NavigationHelper, que se proporciona en algunas plantillas,
            // el evento se controla automáticamente.
        }

        private void image1_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] arrayUri = {
                    "http://img1.wikia.nocookie.net/__cb20120807152321/battlefield/images/4/4e/Bugs_bunny.jpg",
                    "http://codeworks.it/blog/wp-content/uploads/2014/05/Untitled.png",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    ""
                };
                var x = 0;
                for (var i = 0; i < 10; i++)
                {
                    var uri = arrayUri[i];
                    Image imagen = new Image
                    {
                        Width = 100,
                        Height = 100,
                        Margin = new Thickness(x, 0, 0, 0),
                        Source = new BitmapImage(new Uri(uri, UriKind.Absolute)),
                        HorizontalAlignment = HorizontalAlignment.Left,

                    };
                    imagen.Tapped += imagenClick;
                    grid1.Children.Add(imagen);
                    x += 105;
                }
            }
            catch (Exception ex)
            {

            }
        }
        void imagenClick(object sender, TappedRoutedEventArgs e)
        {
            var image = (Image)sender;
            bigImage.Source = image.Source;
        }
    }
}
