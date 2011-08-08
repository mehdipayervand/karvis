﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SJ.Core
{
    public abstract class Presenter<TView> where TView : IView
    {
        private TView view;
        protected virtual TView View
        {
            get { return view; }
            set { view = value; }
        }

        protected Presenter(TView view)
        {
            this.view = view;
        }

        protected abstract void ViewInitialized(object sender, System.EventArgs e);
    }
}
