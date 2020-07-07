import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'truncateString'
})
export class TruncateStringPipe implements PipeTransform {
    transform(value: string, limit = 40, completeWords = true, ellipsis = '...') {

        if (value.length < limit)
            return `${value.substr(0, limit)}`;

        if (completeWords) {
            limit = value.substr(0, limit).lastIndexOf(' ');
        }
        return `${value.substr(0, limit)}${ellipsis}`;
    }
}