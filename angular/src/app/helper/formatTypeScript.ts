import { Pipe, PipeTransform } from "@angular/core";

@Pipe({name: 'formatTypeScript'})
export class FormatTypeScript implements PipeTransform {
  transform(value: string): string {
      let rs 
      if(value.includes('!@#$%^&*()'))
      {
          rs = value.split('!@#$%^&*()')[0]
      }
      else
      {
          rs = value
      }
        switch (rs) { 
            case 'Post Wall' : return 'Đăng vào trang cá nhân'
            case 'ReactionPost' : return 'Bày tỏ cảm xúc'
            case 'Comment' : return 'Bình luận'
            case 'ShareWall' : return 'Chia sẻ vào tường'
            case 'PostGroup' : return 'Đăng vào nhóm'
            //////////////////////////////////////////
            case 'PostStatus' : return 'Đăng vào trang cá nhân'
            case 'SharePost' : return 'Chia sẻ bài viết'
            case 'Reacts': return 'Bày tỏ cảm xúc'
            case 'CommentPost': return 'Bình luận vào bài viết'
            case 'ShareGroup' : return 'Chia sẻ vào nhóm'
            case 'SharePostToGroup' : return 'Chia sẻ bài vào nhóm'
            case 'JoinGroupWithURL ' : return 'Tham gia nhóm với URL'
            case 'MessageToPage' : return 'Nhắn tin vô FanPage'
            case 'MessageToFriend' : return 'Nhắn tin cho bạn bè'
            default: return value
        }
    }
}