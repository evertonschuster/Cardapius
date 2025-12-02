import { OidcService } from './oidcService';

describe('OidcService', () => {
  describe('getOidcParamsFromUrl', () => {
    it('parses search params when present', () => {
      const params = OidcService.getOidcParamsFromUrl('http://site/cb?error=invalid&error_description=bad&error_uri=uri');

      expect(params).toEqual({
        error: 'invalid',
        error_description: 'bad',
        error_uri: 'uri',
      });
    });

    it('falls back to hash fragment when search is empty', () => {
      const params = OidcService.getOidcParamsFromUrl('http://site/cb#error=access_denied&error_description=blocked');

      expect(params.error).toBe('access_denied');
      expect(params.error_description).toBe('blocked');
      expect(params.error_uri).toBeNull();
    });
  });

  describe('checkIdpStatus', () => {
    beforeEach(() => {
      jest.resetAllMocks();
    });

    it('returns OK when endpoint is reachable', async () => {
      global.fetch = jest.fn().mockResolvedValue({ ok: true });

      await expect(OidcService.checkIdpStatus('http://idp')).resolves.toBe('OK');
    });

    it('returns HTTP status when response is not ok', async () => {
      global.fetch = jest.fn().mockResolvedValue({ ok: false, status: 503 });

      await expect(OidcService.checkIdpStatus('http://idp')).resolves.toBe('HTTP 503');
    });

    it('returns CORS when error mentions CORS', async () => {
      global.fetch = jest.fn().mockRejectedValue(new Error('CORS policy')); 

      await expect(OidcService.checkIdpStatus('http://idp')).resolves.toBe('CORS');
    });

    it('returns offline with error name when fetch fails', async () => {
      const err = new Error('network down');
      (err as any).name = 'FetchError';
      global.fetch = jest.fn().mockRejectedValue(err);

      await expect(OidcService.checkIdpStatus('http://idp')).resolves.toBe('OFFLINE (FetchError)');
    });
  });
});

