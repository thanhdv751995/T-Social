import { Pipe, PipeTransform } from "@angular/core";

@Pipe({name: 'nameScriptPiPe'})
export class NameScriptPiPe implements PipeTransform {
  transform(value: string): string {
    if(value.length <= 100)
    {
        return value
    }
    else
    {
        return value.slice(0,97).split('!@#$%^&*()').join(' - ')
    }
   }
}