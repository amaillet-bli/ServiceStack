﻿using System;
using System.Collections.Generic;
using ServiceStack.Html;
using ServiceStack.Razor.Templating;
using ServiceStack.ServiceHost;

namespace ServiceStack.Razor
{
    public abstract class ViewPage<TModel> : ViewPageBase<TModel>
    {
        public HtmlHelper<TModel> Html = new HtmlHelper<TModel>();

        public override HtmlHelper HtmlHelper
        {
            get { return Html; }
        }

        public override void SetState(HtmlHelper htmlHelper)
        {
            Html.HttpRequest = htmlHelper.HttpRequest;
        }

        private IViewEngine viewEngine;
        public override IViewEngine ViewEngine
        {
            get { return viewEngine; }
            set
            {
                Html.ViewEngine = viewEngine = value;
            }
        }

        protected ViewPage()
        {
            this.ScopeArgs = new Dictionary<string, object>();
        }

        public override Type ModelType
        {
            get { return typeof(TModel); }
        }

        public override void Init(IRazorViewEngine viewEngine, ViewDataDictionary viewData, IHttpRequest httpReq, IHttpResponse httpRes)
        {
            this.Request = httpReq;
            this.Response = httpRes;
            Html.Init(httpReq, viewEngine, viewData, null);
            if (viewData.Model is TModel)
                this.Model = (TModel)viewData.Model;
            else
                this.ModelError = viewData.Model;
        }

        public virtual bool IsSectionDefined(string sectionName)
        {
            //return this.childSections.ContainsKey(sectionName);
            return false;
        }
    }
}