export class EnumHelper {

    public static enumSelector(definition): any {
          return Object.keys(definition).filter(f => !isNaN(Number(f)))
                    .map(key => ({ value: key, name: definition[key] }));
      }

}