import '@testing-library/jest-dom';

import {
  TextEncoder as NodeTextEncoder,
  TextDecoder as NodeTextDecoder,
} from 'util';

type GlobalEncoders = typeof globalThis & {
  TextEncoder?: typeof NodeTextEncoder;
  TextDecoder?: typeof NodeTextDecoder;
};

const globalEncoders = globalThis as GlobalEncoders;

if (typeof globalEncoders.TextEncoder === 'undefined') {
  globalEncoders.TextEncoder = NodeTextEncoder;
}

if (typeof globalEncoders.TextDecoder === 'undefined') {
  globalEncoders.TextDecoder = NodeTextDecoder;
}
