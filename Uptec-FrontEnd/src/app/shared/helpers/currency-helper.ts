import { CurrencyMaskConfig } from "ng2-currency-mask/src/currency-mask.config";

export class CurrencyHelper {

    public static ToDecimal(input): any {
        if (input === null) return 0;

        input = input.toString().replace(".", "");
        input = input.replace(",", ".");
        return parseFloat(input);
    }

    public static ToPrice(input): any {
        var ret = (input) ? input.toString().replace(".", ",") : null;
        if (ret) {
            var decArr = ret.split(",");
            if (decArr.length > 1) {
                var dec = decArr[1].length;
                if (dec === 1) { ret += "0"; }
            }
        }
        return ret;
    }
}

export const CurrencyMaskConfigHelper: CurrencyMaskConfig = {
    align: "right",
    allowNegative: true,
    decimal: ",",
    precision: 4,
    prefix: "R$ ",
    suffix: "",
    thousands: "."
};

export const CurrencyMaskConfigHelper2: CurrencyMaskConfig = {
    align: "right",
    allowNegative: true,
    decimal: ",",
    precision: 2,
    prefix: "R$ ",
    suffix: "",
    thousands: "."
};