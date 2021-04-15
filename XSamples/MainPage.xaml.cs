using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using Xamarin.Forms;

namespace XSamples
{
    // 教程参考：https://docs.microsoft.com/zh-cn/xamarin/xamarin-forms/user-interface/graphics/skiasharp/
    public partial class MainPage : ContentPage
    {
        private SKPaint blackFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.Black
        };

        private SKPaint whiteStrokePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.White,
            StrokeWidth = 2,
            StrokeCap = SKStrokeCap.Round,
            IsAntialias = true
        };

        SKPaint whiteFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.White
        };

        public MainPage()
        {
            InitializeComponent();

            Device.StartTimer(TimeSpan.FromSeconds(1f / 60), () =>
            {
                skiaView.InvalidateSurface();
                return true;
            });
        }

        private void SkiaView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(SKColors.CornflowerBlue);

            int width = e.Info.Width;
            int height = e.Info.Height;

            // Set transforms
            canvas.Translate(width / 2, height / 2);
            canvas.Scale(width / 200f);

            // Get DateTime
            DateTime dateTime = DateTime.Now;

            // Clock background!
            canvas.DrawCircle(0, 0, 100, blackFillPaint);

            // Hour and minute marks
            for (int angle = 0; angle < 360; angle += 6)
            {
                canvas.DrawCircle(0, -90, angle % 30 == 0 ? 4 : 2, whiteFillPaint);
                canvas.RotateDegrees(6);
            }

            // Hour hand
            canvas.Save();
            canvas.RotateDegrees(30 * dateTime.Hour + dateTime.Minute / 2f);
            whiteStrokePaint.StrokeWidth = 15;
            canvas.DrawLine(0, 0, 0, -50, whiteStrokePaint);
            canvas.Restore();

            // Minute hand
            canvas.Save();
            canvas.RotateDegrees(6 * dateTime.Minute + dateTime.Second / 10f);
            whiteStrokePaint.StrokeWidth = 10;
            canvas.DrawLine(0, 0, 0, -70, whiteStrokePaint);
            canvas.Restore();

            // Second hand
            canvas.Save();
            canvas.RotateDegrees(6 * dateTime.Second);
            whiteStrokePaint.StrokeWidth = 2;
            canvas.DrawLine(0, 10, 0, -80, whiteStrokePaint);
            canvas.Restore();
        }
    }
}