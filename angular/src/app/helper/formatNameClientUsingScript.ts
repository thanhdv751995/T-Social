import { Pipe, PipeTransform } from "@angular/core";

@Pipe({name: 'formatNameClientUsingScript'})
export class FormatNameClientUsingScript implements PipeTransform {
  transform(value: string): string {
      if(value.includes(':+'))
      {
        let result = value.split(":+").join('++');
        let kq = result.split("++")
        return kq[0] + '-' +kq[1].split('+')[0];
      }
      else
      {
          return value;
      }
   }
}