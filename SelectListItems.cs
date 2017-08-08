using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.IO;
using ISC.EC.Download.WF.Common;
using ISC.EC.Download.Entities;

namespace ISC.EC.Download.WF.AT
{
    public sealed class SelectListItems : CodeActivity
    {
        public InArgument<List<string>> inList { set; get; }
        public InArgument<string> inStateTypeID { set; get; }
        //public InArgument<DownloadItem> inDownloadItem { set; get; }
        public OutArgument<List<string>> outList { set; get; }
        
        protected override void Execute(CodeActivityContext context)
        {
            List<string> oldlist = context.GetValue(inList);
            string[] stateArr = context.GetValue(inStateTypeID).Split(',');
            List<string> newlist = new List<string>();

            //DownloadItem di = context.GetValue(inDownloadItem);
            if (oldlist.Count > 0)
            {
                newlist.Add(oldlist[0]);

                if (oldlist[0].Split(',').Length != 24)
                {
                    //string strLog = string.Format("标题长度不一致！ PromID={0}, ItemId={1},Title_Length={2}", di.PromotionId, di.Id ,oldlist[0].Split(',').Length);
                    //WriteLogClass.WriteLog(strLog);
                }

                if (stateArr != null)
                {
                    foreach (var row in oldlist)
                    {
                        bool flag = false;
                        foreach (var statevalue in stateArr)
                        {
                            if (statevalue != "")
                            {
                                if (row.Split(',')[0].Contains(statevalue) && !flag)
                                {
                                    newlist.Add(row);
                                    flag = true;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                //WriteLogClass.WriteLog(string.Format("Key与Prom不匹配，Prom_ID={0}",di.PromotionId));
            }

            
            context.SetValue(outList, newlist);
        }

    }
}
