import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideToastr } from 'ngx-toastr';
import { TokenheaderService } from './Services/tokenheader.service';

export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }),
     provideRouter(routes),
     provideHttpClient(),
     provideAnimations(),
     provideToastr(),
     provideHttpClient(withInterceptorsFromDi()),  
        {
            provide:HTTP_INTERCEPTORS,
            useClass:TokenheaderService,
            multi:true
        },
]
};
