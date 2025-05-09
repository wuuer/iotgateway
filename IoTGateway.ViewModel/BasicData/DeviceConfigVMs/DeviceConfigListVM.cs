﻿using IoTGateway.Model;
using Microsoft.EntityFrameworkCore;
using Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace IoTGateway.ViewModel.BasicData.DeviceConfigVMs
{
    public partial class DeviceConfigListVM : BasePagedListVM<DeviceConfig_View, DeviceConfigSearcher>
    {
        public List<TreeSelectListItem> AllDevices { get; set; }

        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("DeviceConfig", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"BasicData", dialogWidth: 800),
                this.MakeStandardAction("DeviceConfig", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "BasicData", dialogWidth: 800),
                this.MakeStandardAction("DeviceConfig", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "BasicData", dialogWidth: 800),
                this.MakeStandardAction("DeviceConfig", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "BasicData", dialogWidth: 800),
                this.MakeStandardAction("DeviceConfig", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "BasicData", dialogWidth: 800),
                //this.MakeStandardAction("DeviceConfig", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "BasicData", dialogWidth: 800),
                //this.MakeStandardAction("DeviceConfig", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "BasicData", dialogWidth: 800),
                this.MakeStandardAction("DeviceConfig", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "BasicData"),
            };
        }

        protected override void InitListVM()
        {
            AllDevices = DC.Set<Device>().AsNoTracking()
                .OrderBy(x => x.Parent.Index).ThenBy(x => x.Parent.DeviceName)
                .OrderBy(x => x.Index).ThenBy(x => x.DeviceName)
                .GetTreeSelectListItems(Wtm, x => x.DeviceName);

            var deviceService = Wtm.ServiceProvider.GetService(typeof(DeviceService)) as DeviceService;
            foreach (var device in AllDevices)
            {
                foreach (var item in device.Children)
                {
                    var deviceThread = deviceService.DeviceThreads.Where(x => x.Device.ID.ToString() == (string)item.Value).FirstOrDefault();
                    if (deviceThread != null)
                        item.Icon = deviceThread.Device.AutoStart ? (deviceThread.Driver.IsConnected ? "layui-icon layui-icon-link" : "layui-icon layui-icon-unlink") : "layui-icon layui-icon-pause";

                    item.Text = " " + item.Text;
                    item.Expended = true;
                    item.Selected = false;
                }
            }
            base.InitListVM();
        }

        protected override IEnumerable<IGridColumn<DeviceConfig_View>> InitGridHeader()
        {
            return new List<GridColumn<DeviceConfig_View>>{
                this.MakeGridHeader(x => x.DeviceConfigName).SetWidth(100),
                this.MakeGridHeader(x => x.DataSide).SetWidth(100),
                this.MakeGridHeader(x => x.Description).SetWidth(120),
                this.MakeGridHeader(x => x.Value).SetWidth(100),
                this.MakeGridHeader(x => x.DeviceName_view).SetWidth(100),
                this.MakeGridHeader(x => x.EnumInfo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<DeviceConfig_View> GetSearchQuery()
        {
            var query = DC.Set<DeviceConfig>()
                .CheckContain(Searcher.DeviceConfigName, x => x.DeviceConfigName)
                .CheckContain(Searcher.Value, x => x.Value)
                .CheckEqual(Searcher.DeviceId, x => x.DeviceId)
                .CheckEqual(Searcher.DataSide, x => x.DataSide)
                .Select(x => new DeviceConfig_View
                {
                    ID = x.ID,
                    DeviceConfigName = x.DeviceConfigName,
                    DataSide = x.DataSide,
                    Description = x.Description,
                    Value = x.Value,
                    EnumInfo = x.EnumInfo,
                    DeviceName_view = x.Device.DeviceName,
                })
                .OrderBy(x => x.DeviceName_view).ThenBy(x => x.DeviceConfigName);
            return query;
        }
    }

    public class DeviceConfig_View : DeviceConfig
    {
        [Display(Name = "DeviceName")]
        public String DeviceName_view { get; set; }
    }
}