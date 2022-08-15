import { Pipe, PipeTransform } from "@angular/core";

@Pipe({name: 'formatNameProfile'})
export class FormatNameProfile implements PipeTransform {
  transform(value: string): string {
    var result = value;
    var rs
    if(result.length < 14)
    {
        rs = result;
    }
    else
    {
        rs = result.slice(0,11) + '...'
    }
    return rs
   }
}