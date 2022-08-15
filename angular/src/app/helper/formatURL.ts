import { Pipe, PipeTransform } from "@angular/core";

@Pipe({name: 'formatURL'})
export class FormatUrl implements PipeTransform {
  transform(value: string): string {
    var result = value.split("!@#$%^&*()");
    return result[2];
   }
}