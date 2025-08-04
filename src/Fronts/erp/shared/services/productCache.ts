import initSqlJs, { Database } from 'sql.js';
import { Product } from '@shared/types';

export class ProductCache {
  private dbPromise: Promise<Database>;

  constructor() {
    this.dbPromise = initSqlJs().then(SQL => {
      const stored = localStorage.getItem('product-db');
      const db = stored ? new SQL.Database(Uint8Array.from(JSON.parse(stored))) : new SQL.Database();
      db.run('CREATE TABLE IF NOT EXISTS products (id TEXT PRIMARY KEY, name TEXT, quantity INTEGER, price REAL);');
      return db;
    });
  }

  private async persist(db: Database) {
    const data = db.export();
    localStorage.setItem('product-db', JSON.stringify(Array.from(data)));
  }

  async cacheProducts(products: Product[]) {
    const db = await this.dbPromise;
    const stmt = db.prepare('INSERT OR REPLACE INTO products (id, name, quantity, price) VALUES (?, ?, ?, ?);');
    db.run('BEGIN TRANSACTION;');
    for (const p of products) {
      stmt.run([p.id, p.name, p.quantity, p.price ?? 0]);
    }
    db.run('COMMIT;');
    stmt.free();
    await this.persist(db);
  }

  async getProducts(): Promise<Product[]> {
    const db = await this.dbPromise;
    const result: Product[] = [];
    const stmt = db.prepare('SELECT id, name, quantity, price FROM products;');
    while (stmt.step()) {
      const [id, name, quantity, price] = stmt.get();
      result.push({ id: String(id), name: String(name), quantity: Number(quantity), price: Number(price) });
    }
    stmt.free();
    return result;
  }
}
