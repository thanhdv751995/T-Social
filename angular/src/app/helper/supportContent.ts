import { Pipe, PipeTransform } from "@angular/core";

@Pipe({name: 'supportContent'})
export class SupportContent implements PipeTransform {
  transform(value: string): string {
    var result = value.split("!@#$%^&*()");
    return result[1];
   }
}