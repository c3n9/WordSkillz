namespace WordSkillz;

using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Widget;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;

internal class CustomShellItemRenderer : ShellItemRenderer
{
    public CustomShellItemRenderer(IShellContext context) : base(context)
    {
    }

    public override View? OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
    {
        var view = base.OnCreateView(inflater, container, savedInstanceState);
        if (Context is not null && ShellItem is CustomTabBar { CenterViewVisible: true } tabBar)
        {
            var rootLayout = new FrameLayout(Context)
            {
                LayoutParameters =
                    new FrameLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent)
            };

            rootLayout.AddView(view);
            const int middleViewSize = 170;
            var middleViewLayoutParams = new FrameLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent,
                                                                      ViewGroup.LayoutParams.WrapContent,
                                                                      GravityFlags.CenterHorizontal |
                                                                      GravityFlags.Bottom)
            {
                BottomMargin = 200,
                Width = middleViewSize,
                Height = middleViewSize
            };
            var middleView = new Button(Context)
            {
                LayoutParameters = middleViewLayoutParams,
            };
            middleView.SetTextColor(Android.Graphics.Color.White);
            middleView.Click += delegate
            {
                tabBar.CenterViewCommand?.Execute(null);
            };
            middleView.SetPadding(0, 0, 0, 0);

            // Создание кругового фона для кнопки
            var backgroundDrawable = new GradientDrawable();
            backgroundDrawable.SetShape(ShapeType.Oval);
            backgroundDrawable.SetColor(tabBar.CenterViewBackgroundColor.ToPlatform()); // Установка цвета фона круга
            middleView.SetBackground(backgroundDrawable);
            middleView.SetTextColor(Android.Graphics.Color.White);
            tabBar.CenterViewImageSource?.LoadImage(Application.Current!.MainPage!.Handler!.MauiContext!, result =>
            {
                if (result?.Value is not BitmapDrawable drawable || drawable.Bitmap is null)
                {
                    return;
                }

                const int padding = 20;
                middleView.LayoutParameters = new FrameLayout.LayoutParams(
                    drawable.Bitmap.Width - padding, drawable.Bitmap.Height - padding,
                    GravityFlags.CenterHorizontal | GravityFlags.Bottom)
                {
                    BottomMargin = middleViewLayoutParams.BottomMargin + (int)(1.5 * padding)
                };
                middleView.SetBackground(drawable);
                middleView.SetMinimumHeight(0);
                middleView.SetMinimumWidth(0);
            });

            rootLayout.AddView(middleView);
            return rootLayout;
        }

        return view;
    }
}
