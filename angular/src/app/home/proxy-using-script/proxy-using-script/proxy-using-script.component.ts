import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ActiviteChartService } from 'src/serivce/ActiviteCharts/activite-chart.service';
import { EnumService } from 'src/serivce/Enum/enum.service';
import { ProxyUsingScriptService } from 'src/serivce/proxy-using-script/proxy-using-script.service';
import { ScriptService } from 'src/serivce/scripts/script.service';
import { SignalRService } from 'src/serivce/signalR/signalR.service';
import { TDSMessageService, TDSTableQueryParams } from 'tmt-tang-ui';

@Component({
  selector: 'app-proxy-using-script',
  templateUrl: './proxy-using-script.component.html',
  styleUrls: ['./proxy-using-script.component.css'],
})
export class ProxyUsingScriptComponent implements OnInit, AfterViewInit {
  InfoLoading = false
  clientId = '';
  scriptName = '';
  type = '';
  active = '';
  listProxyUsingScript: any;
  totalProxyActive = 0;
  totalActive = 0;
  totalScript = 0;
  listEnumType: any;
  isVisible = false;

  listUsernameFilter: any;
  listScriptFilter: any;
  listActiveFilter = [true, false];
  listTypeFilter: any;
  listRepeatOrRevokeScript: any = [];
  isRepeatScript: any;
  isShowProfile =false;
  connectionHub:any
  clientUsingScriptId = ''
  errorMessage = ''
  pageSize = 12;
  pageIndex = 1
  skipCount = 0
  listNameSeeding : any;
  nameSeeding =''
  constructor(
    private proxyUsingScriptService: ProxyUsingScriptService,
    private message: TDSMessageService,
    private scriptService: ScriptService,
    private enumService: EnumService,
    private signalRService: SignalRService,
    private activityService : ActiviteChartService
  ) { }

  ngAfterViewInit(): void {
    this.getListEnumType();
  }

  ngOnInit() {
    this.connectionHub = this.signalRService.connection;
    this.listenNewActivity()
    this.getListNameSeeding()
  }
  getListNameSeeding()
  {
    this.activityService.getListNameSeeding().subscribe(res => {
      this.listNameSeeding = res
    })
  }
  getListEnumType() {
    this.enumService.getListEnumType().subscribe((data) => {
      console.log(data);
      
      this.listEnumType = data;
    });
  }
  ngModelChangeSeeding(value)
  { 
    if (value == null) {
      this.nameSeeding = ''
    }
    else {
      this.nameSeeding = value;
    }
    this.getListProxyUsingScripts();
  }
  // filterCampaign(value : string)
  // {
  //   this.seedingName = value
  //   this.getListProxyUsingScripts()
  // }
  onQueryParamsChange(params: TDSTableQueryParams): void {
    this.skipCount = (params.pageIndex - 1) * params.pageSize;
    this.getListProxyUsingScripts()
  }
  getListProxyUsingScripts() {
    this.InfoLoading = true
    this.proxyUsingScriptService
      .getListProxyUsingScripts(
        this.clientId,
        this.nameSeeding,
        this.scriptName,
        this.type,
        this.active,
        this.skipCount,
        this.pageSize
      )
      .subscribe((data) => {
        console.log(data);
        
        this.listProxyUsingScript = data;
        this.InfoLoading = false
      });
  }
  updateActive(proxyUsingScript: any) {
    
    this.proxyUsingScriptService
      .updateActive({ id: proxyUsingScript.id })
      .subscribe(() => {
        this.message.success(
          (proxyUsingScript.isActive ? 'Deactivate ' : 'Active ') +
          proxyUsingScript.scriptName +
          ' successfully'
        );
        let index = this.listProxyUsingScript.items.findIndex(
          (x: any) => x.id == proxyUsingScript.id
        );
        proxyUsingScript.isActive = !proxyUsingScript.isActive;
        this.listProxyUsingScript.items[index] = proxyUsingScript;
      });
    !proxyUsingScript.isActive ? this.totalActive++ : this.totalActive--;
  }

  deleteProxyUS(data: any) {
    this.proxyUsingScriptService.delete(data.id).subscribe(() => {
      this.message.success('Deleted ' + data.scriptName + ' successfully');
      this.getListProxyUsingScripts();
    });
  }

  ngModelChangeIP(event: any) {
      this.clientId = event;
      this.getListProxyUsingScripts();
  }
  ngModelChangeScript(event: any) {
    this.scriptName = event;
    this.getListProxyUsingScripts();
  }

  ngModelChangeType(event: any) {
    this.type = event;
    this.getListProxyUsingScripts();
  }

  ngModelChangeActive(event: any) {
    this.active = event;
    this.getListProxyUsingScripts();
  }

  repeatOrRevokePUS(isActive: boolean) {
    this.proxyUsingScriptService
      .updateRepeatOrRevokePUS(isActive, this.listRepeatOrRevokeScript)
      .subscribe(() => {
        this.listActiveFilter = undefined;
        this.getListProxyUsingScripts();
      });
  }

  showModal(event: any): void {
    this.isRepeatScript = event;
    this.isVisible = true;
  }

  handleOk(): void {
    this.repeatOrRevokePUS(this.isRepeatScript);
    this.isVisible = false;
  }

  handleCancel(): void {
    this.listRepeatOrRevokeScript = [];
    this.isVisible = false;
  }
  //view profile
  ViewProfile(){
    this.isShowProfile = !this.isShowProfile
  }

  //checkContent
    checkContent(value : string)
    {
      if(value.includes('facebook.com'))
      {
        return value
      }
      else
      {
        return 'https://www.facebook.com/'+ value
      }
    }

    checkTypeScript(value : string)
    { 
      if(value == 'ReactionPost')
      {
        return 'Reacts'
      }
      else if(value == 'ShareWall' || value == 'CommentPost' || value == 'SharePostToGroup'|| value == 'SharePost')
      {
        return 'Different'
      }
      else
      {
        return 'Default'
      }
    }
    ///listen signalR from extension for update error
    listenNewActivity() {
      this.connectionHub.on(
        'UpdateErrorDetail',
        (clientUsingScriptId :string , errorMessage : string) => {
          console.log(clientUsingScriptId);
          console.log(errorMessage);
          this.listProxyUsingScript.items.map((x , index) => {
            if(x.id === clientUsingScriptId)
            {
              this.listProxyUsingScript.items[index].errorDetail = errorMessage
            }
          })
        }
      );
    }
}
