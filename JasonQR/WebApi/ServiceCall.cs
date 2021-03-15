using System;
using RestSharp;
using static JasonQR.ViewModels.AuthenticationViewModel;
using static JasonQR.ViewModels.LoginViewModel;

namespace JasonQR.WebApi
{
    public class ServiceCall
    {

        public string loginResponse { get; set; }

        public event LoginEventHandler loginEvent;

        public delegate void LoginEventHandler(ServiceCall sender, EventArgs e);

        public void GetLoginDetails(LoginPostData data)
		{
            string urlStr = "https://jcncars.com/qrcode";
            var client = new RestClient(urlStr);
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(data);

            client.ExecuteAsync(request, response =>
            {
                this.loginResponse = response.Content;
                loginEvent(this, new EventArgs());
            });
        }


        public string authResponse { get; set; }

        public event AuthEventHandler authEvent;

        public delegate void AuthEventHandler(ServiceCall sender, EventArgs e);

        public void GetAuthenticationDetails(AuthPostData authData)
        {
            string urlStr = "https://jcncars.com/qrcode";
            var client = new RestClient(urlStr);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(authData);

            client.ExecuteAsync(request, response =>
            {
                this.authResponse = response.Content;
                authEvent(this, new EventArgs());
            });
        }


        public string qrcodeResponse { get; set; }

        public event QrCodeEventHandler qrcodeEvent;

        public delegate void QrCodeEventHandler(ServiceCall sender, EventArgs e);

        public void QRCodeScanned(AuthPostData authData)
        {
            string urlStr = "https://jcncars.com/qrcode";
            var client = new RestClient(urlStr);
            var request = new RestRequest(Method.POST);
            //authData.text = "Click send to learn about this vehicle QR-3505 55G432";
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(authData);

            Console.WriteLine(request.ToString());
            Console.WriteLine(authData.ToString());

            client.ExecuteAsync(request, response =>
            {
                this.qrcodeResponse = response.Content;
                //Console.WriteLine(response.ToString());
                qrcodeEvent(this, new EventArgs());
            });
        }


        /*public string qrcodeResponse { get; set; }

        public event QrCodeEventHandler qrcodeEvent;

        public delegate void QrCodeEventHandler(ServiceCall sender, EventArgs e);

        public void QRCodeScanned(AuthPostData authData)
        {
            string urlStr = "https://jcncars.com/qrcode";
            var client = new RestClient(urlStr);
            var request = new RestRequest(Method.GET);
            authData.text = "Click send to learn about this vehicle QR-3505 55G432";
            request.AddJsonBody(authData);

            client.ExecuteAsync(request, response =>
            {
                this.qrcodeResponse = response.Content;
                qrcodeEvent(this, new EventArgs());
            });
        }*/

    }
}
