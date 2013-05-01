using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;

namespace JRICO.CodeArea
{
    public delegate IEnumerable MustAddARowHandler(IEnumerable data);


    public class GridViewAlwaysShow : GridView
    {

        //Flag used to identify if the datasource is empty.
        bool _isEmpty = false;

        protected override void OnDataBound(EventArgs e)
        {

            //if in DesignMode, don't do anything special. Just call base and return.
            if (DesignMode)
            {
                base.OnDataBound(e);
                return;
            }

            //hide the dummy row.
            if (_isEmpty)
            {
                Rows[0].Visible = false;
            }
            base.OnDataBound(e);
        }

        protected override void PerformDataBinding(IEnumerable data)
        {

            //If in DesignMode, don't do anything special. Just call base and return.
            if (DesignMode)
            {
                base.PerformDataBinding(data);
                return;
            }

            //Count the data items.(I wish I knew a better way to do this.)
            int objectItemCount = 0;
            foreach (object o in data)
            {
                objectItemCount++;
            }

            //If there is a count, don't do anything special. Just call base and return.
            if (objectItemCount > 0)
            {
                base.PerformDataBinding(data);
                return;
            }

            //Set these values so the GridView knows what's up.
            SelectArguments.TotalRowCount++;
            _isEmpty = true;

            //If it's a DataView, it will work without having to handle the MustAddARow event.
            if (data.GetType() == typeof(DataView))
            {
                //Add a row and use that new view.
                DataView dv = (DataView)data;
                dv.Table.Rows.InsertAt(dv.Table.NewRow(), 0);
                base.PerformDataBinding(dv.Table.DefaultView);
                return;
            }
            else
            {
                //If you are using some custom object, you need to handle this event.
                base.PerformDataBinding(OnMustAddARow(data));
                return;
            }
        }


        protected IEnumerable OnMustAddARow(IEnumerable data)
        {
            if (MustAddARow == null)
            {
                throw new NullReferenceException("The datasource has no rows. You must handle the \"MustAddARow\" Event.");
            }
            return MustAddARow(data);
        }

        public event MustAddARowHandler MustAddARow;

    }

}