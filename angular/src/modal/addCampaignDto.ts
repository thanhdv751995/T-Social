export interface addCampaignDto 
{
    name?: string ;
    postsWall: number;
    postsGroup: number;
    comments: number ;
    reacts: number;
    sharesWall: number;
    sharesGroup: number;
    groupType: number;
    seedingContentDtos: [
      {
        content: string;
        imageUrl: string
      }
    ]
  }