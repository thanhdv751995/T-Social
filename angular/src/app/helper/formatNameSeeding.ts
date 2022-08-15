import { Pipe, PipeTransform } from "@angular/core";

@Pipe({ name: 'nameSeeding' })
export class NameSeeding implements PipeTransform {
    transform(value: string): string {
        return value.length <= 15 ? value : value.slice(0, 13) + "...";
       
    }
}