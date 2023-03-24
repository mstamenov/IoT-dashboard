import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { ChartModule } from "primeng/chart";
import { FlexLayoutModule } from '@angular/flex-layout';
import {MatCardModule} from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button';
import {MatBadgeModule} from '@angular/material/badge';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatRippleModule} from '@angular/material/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
  ],
  imports: [
    HttpClientModule,
    BrowserAnimationsModule,
    ChartModule,
    FlexLayoutModule,
    MatCardModule,
    MatButtonModule,
    MatBadgeModule,
    MatCheckboxModule,
    MatRippleModule
  ],
  exports:[
    HttpClientModule,
    BrowserAnimationsModule,
    ChartModule,
    FlexLayoutModule,
    MatCardModule,
    MatButtonModule,
    MatBadgeModule,
    MatCheckboxModule,
    MatRippleModule

  ],
  providers: []
})
export class CoreModule {}
