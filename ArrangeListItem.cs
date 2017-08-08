using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
//using System.IO;
//using ISC.EC.Download.WF.Common;
//using ISC.EC.Download.Entities;

namespace ISC.EC.Download.WF.AT
{
    public sealed class ArrangeListItem : CodeActivity
    {
        public InOutArgument<List<string>> List { set; get; }

        public OutArgument<bool> HasChanged { get; set; }
        //public InArgument<DownloadItem> inDownloadItem { set; get; }

        protected override void Execute(CodeActivityContext context)
        {
            List<string> list = context.GetValue(List);
            //DownloadItem di = context.GetValue(inDownloadItem);
            bool flag = false;
            int index = 0;
            
            foreach(var row in list)
            {
                if (!row.Contains(","))
                {
                    index++;
                }
                else break;
            }

            /*/-----------------------------------------
             
              
            if(list[4].Contains("ERROR"))
            {
                string str = string.Empty;
                foreach (var row in context.GetValue(List))
                {
                    
                    if (!row.Equals(""))
                    {
                        str += row;
                        str += ",";
                    }
                    
                }
                str += string.Format("itemID={0}, promID={1}", di.Id, di.PromotionId);
                WriteLogClass.WriteLog(str);
                
            }
            //-----------------------------------------*/

            if (index > 0)
            {
                list.RemoveRange(0, index);
                flag = true;
            }
            

            context.SetValue(List, list);
            context.SetValue(HasChanged, flag);
        }
    }
}
