import { Pipe, PipeTransform } from "@angular/core";

@Pipe({name: 'formatLink'})
export class formatLink implements PipeTransform {
  transform(value: string): string {
     if(value.includes('https://mbasic.facebook.com/'))
     {
      var result = value.replace("https://mbasic.facebook.com/",'https://facebook.com/');
      return result;
     }
     else
     {
      return value;
     }
   }
}