import { Pipe, PipeTransform } from "@angular/core";

@Pipe({name: 'formatActivity'})
export class FormatActivity implements PipeTransform {
  transform(value: string): string {
    var result = value.split("!@#$%^&*()");
    var rs
    if(result[0].length < 110)
    {
        rs = result[0];
    }
    else
    {
        rs = result[0].slice(0,107) + '...'
    }
    return rs
   }
}