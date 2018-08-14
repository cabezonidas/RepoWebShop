export interface ILunch {
    lunchId: number;
    // public IEnumerable<LunchItem> Items { get; set; }
    // public IEnumerable<LunchMiscellaneous> Miscellanea { get; set; }
    comments: string;
    isCombo: boolean;
    isGeneric: boolean;
    title: string;
    description: string;
    thumbnailUrl: string;
    eventDuration: number;
    attendants: number;
}
