import React from 'react';
import { QuizOption, OptionId } from '../types';

interface Props {
  option: QuizOption;
  selected: boolean;
  disabled?: boolean;
  reveal?: boolean;
  isCorrect?: boolean;
  wasSelected?: boolean;
  onClick: (id: OptionId) => void;
}

export const AlternativeButton = ({ option, selected, disabled, reveal, isCorrect, wasSelected, onClick }: Props) => {
  const classes = ['option-btn'];
  if (selected) classes.push('selected');
  if (reveal && isCorrect) classes.push('correct');
  if (reveal && wasSelected && !isCorrect) classes.push('wrong');

  return (
    <button type="button" className={classes.join(' ')} disabled={disabled} onClick={() => onClick(option.id)}>
      <span className="option-id">{option.id}</span>
      <span>{option.text}</span>
    </button>
  );
};
