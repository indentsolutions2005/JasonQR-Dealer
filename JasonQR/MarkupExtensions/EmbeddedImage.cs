using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace jasonQR
{
    [ContentProperty("ResourceId")]
    public class EmbeddedImage : IMarkupExtension
    {
        public string ResourceId { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (!String.IsNullOrWhiteSpace(ResourceId))
            {
                return ImageSource.FromResource(ResourceId);
            }
            return null;
        }
    }
}
