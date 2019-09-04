using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GridX
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        // inputs
        double ibo;
        double drawlen;
        double drawwt;
        double arrowwt;
        // outputs
        double fps;
        double ke;
        double mv;
       
        public MainPage()
        {
            InitializeComponent();
            
        }

        // async void OnCalcButtonClicked(object sender, EventArgs args)
        async void OnCalcButtonClicked(object sender, EventArgs args)
        {
            //await label.RelRotateTo(360, 1000);
            //await Debug.WriteLine("KO");
            // await IBO.Text;
            var nameValue = IBO.Text;
            Debug.WriteLine(nameValue);
            Debug.WriteLine(IBO.Text);
            Debug.WriteLine(DrawLength.Text);
            Debug.WriteLine(DrawWeight.Text);
            Debug.WriteLine(ArrowWeight.Text);

 
            //Debug.WriteLine(sIBO);
            //Debug.WriteLine(sDrawLength);
            //Debug.WriteLine(sDrawWeight);
            // Debug.WriteLine(sArrowWeight);

            // gather ye rosebuds while yo may
            ibo = drawlen = drawwt = arrowwt = 0.0;
            bool b1 = Double.TryParse(IBO.Text, out ibo);
            bool b2 = Double.TryParse(DrawLength.Text, out drawlen);
            bool b3 = Double.TryParse(DrawWeight.Text, out drawwt);
            bool b4 = Double.TryParse(ArrowWeight.Text, out arrowwt);

            if (!b1 || !b2 || !b3 || !b4)
            {
                //DisplayAlert("Data Input Error", "The data is either missing or invalid.\nPlease try again.", "OK");
                await DisplayAlert("Data Input Error", "The data is either missing or invalid.\nPlease try again.", "OK");
                // public System.Threading.Tasks.Task DisplayAlert(string title, string message, string cancel);
                //System.Threading.Tasks.Task DisplayAlert("Data Input Error", "The data is either missing or invalid.\nPlease try again.", "OK");

                return;
            }

            // calculate
            fps = FPS(ibo, drawlen, drawwt, arrowwt);
            ke = KE(fps, arrowwt);
            mv = MV(fps, arrowwt);

            fps = Math.Round(fps, 0);
            ke = Math.Round(ke, 0);
            mv = Math.Round(mv, 2);

            // ouput results
            // TrueArrowSpeed.Text = String.Format("\n{0} fps", fps);
            //KineticEnergy.Text = String.Format("\n{0} lb-ft2/sec2", ke);
            // Momentum.Text = String.Format("\n{0} lb-ft or slugs", mv);

            // ouput results 
            TrueArrowSpeed.Text = String.Format("{0}", fps);
            KineticEnergy.Text = String.Format("{0}", ke);
            Momentum.Text = String.Format("{0}", mv);
        }


        // private void Clicked()
        /*
        private void submit_Clicked(object sender, EventArgs e)
        {
            //var nameValue = IBO.Text;
            //var nameValue = IBO.Text;

            Debug.WriteLine("KO");
           
        }
        */

        //    private void submit_Clicked(object sender, EventArgs e)
        //    {
        //       Entry entry = e as Entry;
        //       var text = entry.Text;
        //   }



  

    /// http://bestcompoundbowsource.com/whats-bows-real-speed/
    /// Draw Length - for every 1"< 30" subtract 10 FPS
    /// Draw Weight - for every 10 lbs < 70 lbs subtract 15 - 20 FPS
    ///	Arrow Weight - for every 5 grains > 350 grains subtract 1.5 FPS from IBO
    ///	Xtra String Accessories - subtract 5 - 6 FPS
    ///	Human Release Factor - subtract 2 - 3 FPS
    private double FPS(double IBO, double drawlen, double drawwt, double arrowwt)
    {
        double fps = IBO - (30.0 - drawlen) * 10.0;
        fps = fps - 20.0 * (70.0 - drawwt) / 10.0;
        fps = fps - 1.5 * (arrowwt - 350.0) / 5.0;
        fps = fps - 6.0;
        fps = fps - 3.0;
        return fps;
    }

    private double KE(double speed, double weight)
    {
        // KE = 1/2 m * v^2
        // 1 grain = 0.000142857 pound
        // 1 pound = 6999.999691 grains
        // double vfps = speed;
        // double wtgr = weight;
        // double wtlb = wtgr * 0.000142857;
        // correlates well with https://www.realtree.com/kinetic-energy-and-momentum-calculator
        // also with http://archerycalculator.com/estimate-bow-speed/
        double ke = speed * speed * weight / 450800;
        //double ke = vfps * vfps * wtlb / 2.0;
        return ke;
    }

    private double MV(double speed, double weight)
    {
        // correlates well with https://www.realtree.com/kinetic-energy-and-momentum-calculator
        // also with http://archerycalculator.com/estimate-bow-speed/
        double mv = speed * weight / 225400;
        return mv;
    }

    }


}
