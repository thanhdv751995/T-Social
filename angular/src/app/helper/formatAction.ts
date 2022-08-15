import { Pipe, PipeTransform } from "@angular/core";

@Pipe({name: 'formatNameAction'})
export class formatAction implements PipeTransform {
  transform(value: string): string {
      var result = value.split(":+");
      return result[0];
   }
}