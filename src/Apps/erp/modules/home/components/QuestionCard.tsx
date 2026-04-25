import React from 'react';
import { Question, OptionId } from '../types';
import { AlternativeButton } from './AlternativeButton';

interface Props {
  question: Question;
  selectedOptionId: OptionId | null;
  onSelect: (id: OptionId) => void;
  disabled?: boolean;
  reveal?: boolean;
}

export const QuestionCard = ({ question, selectedOptionId, onSelect, disabled, reveal }: Props) => (
  <section className="card question-card">
    <header className="question-header">
      <span className="pill">{question.topic}</span>
      <span className="pill difficulty">Dificuldade: {question.difficulty}</span>
    </header>

    <h2>Questão {question.id}</h2>
    <p className="statement">{question.statement}</p>

    <div className="options-grid">
      {question.options.map((option) => (
        <AlternativeButton
          key={option.id}
          option={option}
          selected={selectedOptionId === option.id}
          disabled={disabled}
          reveal={reveal}
          isCorrect={option.id === question.correctOptionId}
          wasSelected={selectedOptionId === option.id}
          onClick={onSelect}
        />
      ))}
    </div>
  </section>
);
