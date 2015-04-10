using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace MVCControls
{
    /// <summary>
    /// Html扩展信息
    /// </summary>
    public static class HtmlGridExtention
    {
        /// <summary>
        ///  返回表格信息
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="containerId"></param>
        /// <returns></returns>
        public static GridOption GridView(this HtmlHelper helper, string containerId)
        {
            return new GridOption(containerId);
        }

        /// <summary>
        /// 加载表格信息
        /// </summary>
        /// <param name="opt"></param>
        /// <returns></returns>
        public static MvcHtmlString ToGrid(this GridOption opt)
        {
            HttpContext.Current.Items["is_osgrid"] = "y";
            StringBuilder strOptJson = new StringBuilder();
            strOptJson.Append("<script type=\"text/javascript\">");
            strOptJson.Append("$(function() {");
            strOptJson.Append("$(\"#page-list\").osgrid(");
            strOptJson.Append(JsonConvert.SerializeObject(opt));
            strOptJson.Append(");");
            strOptJson.Append("});");
            strOptJson.Append(" </script>");
            return new MvcHtmlString(strOptJson.ToString());
        }
    }



    /// <summary>
    ///  表格信息
    /// </summary>
    public class GridOption
    {
        public GridOption(string containerId)
        {
            ContainerId = containerId;
            Headers=new List<GridColumn>();
            Methods=new OSGridMethods();
            IsDisplayHeader = true;
        }

        /// <summary>
        /// 是否显示头部
        /// </summary>
        public bool IsDisplayHeader { get; set; }

        /// <summary>
        /// 包含容器的ID
        /// </summary>
        public string ContainerId { get; private set; }

        #region  列头信息

        /// <summary>
        /// 头信息
        /// </summary>
        public List<GridColumn> Headers { get; private set; }

        /// <summary>
        /// 绑定列表的列信息
        /// </summary>
        /// <returns></returns>
        public GridOption BindColumns(Action<GridOption> action)
        {
            action(this);
            return this;
        }

        /// <summary>
        /// 创建一个新列
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public GridColumn Column(string columnName)
        {
            var column = new GridColumn(columnName);
            this.Headers.Add(column);
            return column;
        }

        #endregion

        #region 分页信息

        /// <summary>
        /// 头信息
        /// </summary>
        public GridPage Page { get; private set; }


        /// <summary>
        /// 设置分页信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public GridOption SetPage(int pageSize)
        {
            Page = new GridPage(pageSize);
            return this;
        }
        #endregion

        /// <summary>
        /// 设置是否显示头部
        /// </summary>
        /// <returns></returns>
        public GridOption SetIsDisplayHeader(bool isDisplay)
        {
            IsDisplayHeader=isDisplay;
            return this;
        }


        #region  高级配置信息
        /// <summary>
        /// 高级配置信息
        /// </summary>
        public OSGridMethods Methods { get; private set; }

        /// <summary>
        /// 设置高级信息
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        public GridOption SetMethods(Action<OSGridMethods> act)
        {
            act(Methods);
            return this;
        }
        #endregion
    }

    /// <summary>
    ///    表格分页信息 
    /// </summary>
    public class GridPage
    {
        public GridPage()
        {
        }

        /// <summary>
        /// 表格分页信息
        /// </summary>
        /// <param name="pageSize"></param>
        public GridPage(int pageSize)
        {
            IsPage = true;
            PageSize = pageSize;
        }

        /// <summary>
        /// 头标题
        /// </summary>
        public bool IsPage { get; set; }


        private int _pageSize = 20;
        /// <summary>
        /// 头标题
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }
    }


    /// <summary>
    /// 高级设置
    /// </summary>
    public class OSGridMethods
    {
    
        /// <summary>
        /// 扩展传送服务器的参数  方法
        /// </summary>
        public string ExtSendParas { get; set; }

        /// <summary>
        /// 获取数据源方法 
        /// </summary>
        public string GetDataFunc { get; set; }

        /// <summary>
        /// 数据绑定之后执行事件
        /// </summary>
        public string DataBound { get; set; }

        /// <summary>
        /// 头部格式化方法
        /// </summary>
        public string HeaderFormat { get; set; }

        /// <summary>
        /// 行格式化方法
        /// </summary>
        public string RowFormat { get; set; }

        /// <summary>
        /// 表格尾部格式化方法名称
        /// </summary>
        public string FooterFormat { get; set; }


        /// <summary>
        /// 设置扩展传送服务器的参数事件
        /// </summary>
        /// <returns></returns>
        public OSGridMethods SetExtSendParas(string extSendParas)
        {
            ExtSendParas = extSendParas;
            return this;
        }

        /// <summary>
        /// 设置获取数据方法
        /// </summary>
        /// <returns></returns>
        public OSGridMethods SetGetDataFunc(string jsFunc)
        {
            GetDataFunc = jsFunc;
            return this;
        }

        /// <summary>
        /// 设置数据绑定后事件
        /// </summary>
        /// <returns></returns>
        public OSGridMethods SetDataBound(string dataBound)
        {
            DataBound = dataBound;
            return this;
        }

        /// <summary>
        /// 设置头部格式化事件
        /// </summary>
        /// <returns></returns>
        public OSGridMethods SetHeaderFormat(string headerFormat)
        {
            HeaderFormat = headerFormat;
            return this;
        }

        /// <summary>
        /// 设置行格式化事件
        /// </summary>
        /// <returns></returns>
        public OSGridMethods SetRowFormat(string rowFormat)
        {
            RowFormat = rowFormat;
            return this;
        }

        /// <summary>
        /// 设置行格式化事件
        /// </summary>
        /// <returns></returns>
        public OSGridMethods SetFooterFormat(string footerFormat)
        {
            FooterFormat = footerFormat;
            return this;
        }
    }

    /// <summary>
    /// 列实体
    /// </summary>
    public class GridColumn
    {
        /// <summary>
        /// 构造函数   
        /// </summary>
        /// <param name="columnName">列名</param>
        public GridColumn(string columnName)
        {
            ColumnName = columnName;
        }

        /// <summary>
        /// 头标题
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; private set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public string Width { get; private set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public string Class { get; private set; }

        ///// <summary>
        /////   头部格式化方法（js 方法/方法名）
        ///// </summary>
        //public string Format { get; private set; }

        /// <summary>
        ///   内容格式化方法（js 方法/方法名）
        /// </summary>
        public string ContentFormat { get; private set; }

        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="title"></param>
        public GridColumn SetTitle(string title)
        {
            Title = title;
            return this;
        }
        /// <summary>
        /// 设置宽度
        /// </summary>
        /// <param name="width"></param>
        public GridColumn SetWidth(string width)
        {
            Width = width;
            return this;
        }
        public GridColumn SetClass(string className)
        {
            Class = className;
            return this;
        }


        ///// <summary>
        ///// 设置标题 格式化方法或方法名称（js）
        ///// </summary>
        ///// <param name="format"></param>
        //public GridColumn SetFormat(string format)
        //{
        //    Format = format;
        //    return this;
        //}


        /// <summary>
        /// 设置内容 格式化方法或方法名称（js）
        /// </summary>
        /// <param name="contentFormat"></param>
        public GridColumn SetContentFormat(string contentFormat)
        {
            ContentFormat = contentFormat;
            return this;
        }
    }
}
