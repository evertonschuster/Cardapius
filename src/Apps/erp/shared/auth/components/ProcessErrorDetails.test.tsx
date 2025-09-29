import React from 'react';
import { render, screen, waitFor } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { ProcessErrorDetails } from './ProcessErrorDetails';

const mockNavigate = jest.fn();

jest.mock('react-router-dom', () => ({
  useNavigate: () => mockNavigate,
}));

describe('ProcessErrorDetails', () => {
  let originalClipboard: Clipboard | undefined;
  let writeTextMock: jest.Mock;

  beforeEach(() => {
    jest.clearAllMocks();
    originalClipboard = navigator.clipboard;
    writeTextMock = jest.fn().mockResolvedValue(undefined);

    Object.defineProperty(navigator, 'clipboard', {
      configurable: true,
      value: { writeText: writeTextMock },
    });
  });

  afterEach(() => {
    if (originalClipboard) {
      Object.defineProperty(navigator, 'clipboard', {
        configurable: true,
        value: originalClipboard,
      });
    } else {
      delete (navigator as any).clipboard;
    }
  });

  it('renders details and handles actions', async () => {
    const details = {
      title: 'Erro ao autenticar',
      description: 'Falha inesperada',
      traceId: 'trace-123',
      errorUri: 'https://errors/cardapius',
      code: 'server_error',
      requestId: 'req-1',
      timestamp: '2024-01-01T00:00:00Z',
      authority: 'https://authority',
      clientId: 'client-id',
      redirectUri: 'https://redirect',
    };

    const onRetry = jest.fn();
    const onHome = jest.fn();
    const user = userEvent.setup();

    render(<ProcessErrorDetails details={details as any} onRetry={onRetry} onHome={onHome} />);

    expect(screen.getByText(details.title)).toBeInTheDocument();
    expect(screen.getByText(details.description)).toBeInTheDocument();
    expect(screen.getByText('ID suporte: trace-123')).toBeInTheDocument();

    await user.click(screen.getByText('Tentar novamente'));
    expect(onRetry).toHaveBeenCalled();

    await user.click(screen.getByText('Voltar para a página inicial'));
    expect(onHome).toHaveBeenCalled();

    await user.click(screen.getByText('Copiar detalhes'));
    await waitFor(() =>
      expect(writeTextMock).toHaveBeenCalledWith(JSON.stringify(details, null, 2)),
    );

    await user.click(screen.getByText('Ver detalhes técnicos'));
    expect(screen.getByText(JSON.stringify(details, null, 2))).toBeInTheDocument();
    expect(screen.getByRole('link', { name: details.errorUri })).toHaveAttribute('href', details.errorUri);
  });

  it('navigates home when no handler is provided', async () => {
    const details = {
      title: 'Erro',
      description: 'Algo deu errado',
    } as any;

    const user = userEvent.setup();

    render(<ProcessErrorDetails details={details} />);

    await user.click(screen.getByText('Voltar para a página inicial'));
    expect(mockNavigate).toHaveBeenCalledWith('/');
  });
});
