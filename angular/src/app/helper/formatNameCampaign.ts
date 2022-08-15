import { Pipe, PipeTransform } from "@angular/core";

@Pipe({name: 'formatNameCampaign'})
export class formatNameCampaign implements PipeTransform {
  transform(value: string): string {
      var result = value.split(":+");
      return result[1].split("+")[0];
   }
}