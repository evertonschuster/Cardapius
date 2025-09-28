import '@testing-library/jest-dom';

import {
  TextEncoder as NodeTextEncoder,
  TextDecoder as NodeTextDecoder,
} from 'util';

type NodeEncoders = {
  TextEncoder: typeof NodeTextEncoder;
  TextDecoder: typeof NodeTextDecoder;
};

const globalWithEncoders = globalThis as typeof globalThis & Partial<NodeEncoders>;

if (typeof globalWithEncoders.TextEncoder === 'undefined') {
  globalWithEncoders.TextEncoder = NodeTextEncoder as NodeEncoders['TextEncoder'];
}

if (typeof globalWithEncoders.TextDecoder === 'undefined') {
  globalWithEncoders.TextDecoder = NodeTextDecoder as NodeEncoders['TextDecoder'];
}
