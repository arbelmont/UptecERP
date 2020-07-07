export class DownloadHelper {

    static handleFile(res: any, mime: string, filename: string) {

        var newBlob = new Blob([res], { type: mime });

                //IE
                if(window.navigator && window.navigator.msSaveOrOpenBlob) {
                    window.navigator.msSaveOrOpenBlob(newBlob);
                }

                const data = window.URL.createObjectURL(newBlob);
                var link = document.createElement('a');

                link.href = data;
                link.download = filename;
                link.dispatchEvent(new MouseEvent('click', { bubbles: true, cancelable: true, view: window }));

                // For Firefox it is necessary to delay revoking the ObjectURL
                setTimeout(function () {
                    window.URL.revokeObjectURL(data);
                    link.remove();
                }, 100);
    }
}