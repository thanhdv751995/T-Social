import { Pipe, PipeTransform } from "@angular/core";

@Pipe({ name: 'nameScript' })
export class NameScript implements PipeTransform {
    transform(value: string): string {
        return value?.length <= 23 ? value : value.slice(0, 20) + "...";
       
    }
}