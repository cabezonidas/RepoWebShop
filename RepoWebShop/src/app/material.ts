import { MatButtonModule, MatCheckboxModule } from '@angular/material';
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { NgModule } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';

@NgModule({
    imports: [MatButtonModule, MatCheckboxModule, MatToolbarModule, MatIconModule, MatCardModule, MatBottomSheetModule],
    exports: [MatButtonModule, MatCheckboxModule, MatToolbarModule, MatIconModule, MatCardModule, MatBottomSheetModule],
})
export class MaterialModule { }
