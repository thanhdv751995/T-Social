import { Pipe, PipeTransform } from "@angular/core";

@Pipe({name: 'formatString'})
export class FomateStringPipe implements PipeTransform {
  transform(value: string): string {
     // return value.replace(/[!@#$%^&*()]/gi,'--');
      return value.split('!@#$%^&*()').join(' - ')
   }
}