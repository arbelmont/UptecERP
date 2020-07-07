import { NoThousandSeparatorPipe } from './pipes/noThousandSeparatorPipe';
import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";
import { ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';

import { SharedService } from './services/shared.service';

import { FooterComponent } from "./footer/footer.component";
import { NavbarComponent } from "./navbar/navbar.component";
import { SidebarComponent } from "./sidebar/sidebar.component";
import { CustomizerComponent } from './customizer/customizer.component';
import { NotificationSidebarComponent } from './notification-sidebar/notification-sidebar.component';
import { ToggleFullscreenDirective } from "./directives/toggle-fullscreen.directive";
import { ScrollFirstInvalid } from './directives/scroll-first-invalid.directive';
import { TruncateStringPipe } from './pipes/TruncateStringPipe';
import { SidebarDirective } from './directives/sidebar.directive';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { SidebarLinkDirective } from './directives/sidebarlink.directive';
import { SidebarListDirective } from './directives/sidebarlist.directive';
import { SidebarAnchorToggleDirective } from './directives/sidebaranchortoggle.directive';
import { SidebarToggleDirective } from './directives/sidebartoggle.directive';
import { ProgressComponent } from './progress/progress.component';

@NgModule({
    exports: [
        CommonModule,
        FooterComponent,
        NavbarComponent,
        SidebarComponent,
        CustomizerComponent,
        ProgressComponent,
        NotificationSidebarComponent,
        ToggleFullscreenDirective,
        SidebarDirective,
        NgbModule,
        TranslateModule,
        ScrollFirstInvalid,
        TruncateStringPipe,
        NoThousandSeparatorPipe
    ],
    imports: [
        RouterModule,
        CommonModule,
        HttpModule,
        ReactiveFormsModule,
        NgbModule,
        TranslateModule,
        PerfectScrollbarModule
    ],
    declarations: [
        FooterComponent,
        NavbarComponent,
        SidebarComponent,
        CustomizerComponent,
        NotificationSidebarComponent,
        ProgressComponent,
        ToggleFullscreenDirective,
        SidebarDirective,
        SidebarLinkDirective,
        SidebarListDirective,
        SidebarAnchorToggleDirective,
        SidebarToggleDirective,
        ScrollFirstInvalid,
        TruncateStringPipe,
        NoThousandSeparatorPipe,
        ProgressComponent
    ],
    providers:[SharedService]
})
export class SharedModule { }
