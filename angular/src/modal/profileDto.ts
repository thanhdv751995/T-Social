export interface profileDto {
    id: string ,
    profileType: string,
    startTime : number,
    duringMinutes : number,
    profileName? : string,
    isUpdate? : boolean,
    listScript? : string[]
}
