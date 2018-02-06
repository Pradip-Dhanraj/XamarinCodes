using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Util;

namespace PradipDhanraj
{
    public class PlayGifView : View
    {
        private static int DEFAULT_MOVIEW_DURATION = 1000;

        private int mMovieResourceId;
        private Movie mMovie;

        private long mMovieStart = 0;
        private int mCurrentAnimationTime = 0;

        public PlayGifView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.Honeycomb)
            {
                SetLayerType(LayerType.Software, null);
            }

        }

        public void setImageResource(int mvId)
        {
            this.mMovieResourceId = mvId;
            mMovie = Movie.DecodeStream(Resources.OpenRawResource(mMovieResourceId));
            RequestLayout();
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            if (mMovie != null)
            {
                SetMeasuredDimension(mMovie.Width(), mMovie.Height());
            }
            else
            {
                SetMeasuredDimension(SuggestedMinimumWidth, SuggestedMinimumHeight);
            }
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            if (mMovie != null)
            {
                updateAnimtionTime();
                drawGif(canvas);
                Invalidate();
            }
            else
            {
                drawGif(canvas);
            }
        }

        private void updateAnimtionTime()
        {
            long now = Android.OS.SystemClock.UptimeMillis();

            if (mMovieStart == 0)
            {
                mMovieStart = now;
            }
            int dur = mMovie.Duration();
            if (dur == 0)
            {
                dur = DEFAULT_MOVIEW_DURATION;
            }
            mCurrentAnimationTime = (int)((now - mMovieStart) % dur);
        }

        private void drawGif(Canvas canvas)
        {
            mMovie.SetTime(mCurrentAnimationTime);
            mMovie.Draw(canvas, 0, 0);
            canvas.Restore();
        }
    }
}