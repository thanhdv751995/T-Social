import { Pipe, PipeTransform } from "@angular/core";

@Pipe({name: 'formatHistory'})
export class FormatHistory implements PipeTransform {
  transform(value: string): string {
      if(value.includes(':+'))
      {
        var result = value.split(":+");
        var nameCampaign = result[1].split("+") 
        switch (result[0]) { 
            case 'Post Wall' : return 'Đăng vào trang cá nhân' + '(' +'chiến dịch ' + nameCampaign[0] +')' 
            case 'Reacts' : return 'Bày tỏ cảm xúc' + '(' +'chiến dịch ' + nameCampaign[0]+')'
            case 'Comment' : return 'Bình luận' + '(' +'chiến dịch ' + nameCampaign[0] +')'
            case 'ShareWall' : return 'Chia sẻ vào trang cá nhân' + '(' +'chiến dịch ' + nameCampaign[0] +')'
            case 'Post Group' : return 'Đăng vào nhóm' + '(' +'chiến dịch' + nameCampaign[0]+')'
            case 'ShareGroup' :return 'Chia sẻ vào nhóm' + '(' +'chiến dịch ' + nameCampaign[0] +')'
            case 'SharePost' :return 'Chia sẻ ' + '(' +'chiến dịch ' + nameCampaign[0] +')'
            default: return value
        }
      }
      else
      {
          return value
      }
    }
}