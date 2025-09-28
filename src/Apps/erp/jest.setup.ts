import '@testing-library/jest-dom';

import { TextEncoder, TextDecoder } from 'util';

if (typeof global.TextEncoder === 'undefined') {
  // @ts-expect-error TextEncoder is intentionally added to the Node test environment
  global.TextEncoder = TextEncoder;
}

if (typeof global.TextDecoder === 'undefined') {
  // @ts-expect-error TextDecoder is intentionally added to the Node test environment
  global.TextDecoder = TextDecoder as typeof global.TextDecoder;
}
