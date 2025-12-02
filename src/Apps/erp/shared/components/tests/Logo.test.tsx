import { render, screen } from '@testing-library/react';
import { Logo } from '../Logo';

describe('Logo', () => {
  it('shows the image and title by default', () => {
    render(<Logo />);

    const image = screen.getByAltText('Cardapius logo');
    expect(image).toBeInTheDocument();
    expect(screen.getByText('Cardapius')).toBeInTheDocument();
  });

  it('can hide the title when requested', () => {
    render(<Logo showText={false} />);

    expect(screen.getByAltText('Cardapius logo')).toBeInTheDocument();
    expect(screen.queryByText('Cardapius')).not.toBeInTheDocument();
  });
});
