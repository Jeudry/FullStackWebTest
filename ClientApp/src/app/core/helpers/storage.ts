import {decrypt, encrypt} from '@core/shared/util/util-encrypt';
import {config} from '@env/config.dev';

export abstract class StorageService implements Storage {
  protected constructor(protected readonly api: Storage) {
  }

  get length(): number {
    return this.api.length;
  }

  setItem(key: string, value: unknown): void {
    let data = JSON.stringify(value);
    if (config.encrypt) {
      data = encrypt(data);
    }
    this.api.setItem(key, data);
  }

  getItem<T>(key: string): T | null {
    const data = this.api.getItem(key);

    if (data !== null) {
      if (config.encrypt) {
        return decrypt<T>(data);
      }

      return JSON.parse(data) as T;
    }

    return null;
  }

  clear(): void {
    this.api.clear();
  }

  key(index: number): string | null {
    return this.api.key(index);
  }

  removeItem(key: string): void {
    this.api.removeItem(key);
  }
}
