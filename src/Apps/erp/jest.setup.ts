import '@testing-library/jest-dom';

import { TextEncoder, TextDecoder } from 'util';

const globalWithEncoders = globalThis as typeof globalThis & {
  TextEncoder?: typeof TextEncoder;
  TextDecoder?: typeof TextDecoder;
};

if (typeof globalWithEncoders.TextEncoder === 'undefined') {
  globalWithEncoders.TextEncoder = TextEncoder;
}

if (typeof globalWithEncoders.TextDecoder === 'undefined') {
  globalWithEncoders.TextDecoder = TextDecoder;
}
