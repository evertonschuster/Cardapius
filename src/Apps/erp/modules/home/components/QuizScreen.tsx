import React from 'react';
import { OptionId, Question, QuizMode } from '../types';
import { FeedbackPanel } from './FeedbackPanel';
import { ProgressBar } from './ProgressBar';
import { QuestionCard } from './QuestionCard';

interface Props {
  mode: QuizMode;
  question: Question;
  index: number;
  total: number;
  selectedOptionId: OptionId | null;
  answered: boolean;
  onSelect: (id: OptionId) => void;
  onConfirm: () => void;
  onNext: () => void;
}

export const QuizScreen = ({
  mode,
  question,
  index,
  total,
  selectedOptionId,
  answered,
  onSelect,
  onConfirm,
  onNext
}: Props) => (
  <section>
    <ProgressBar current={index + (answered ? 1 : 0)} total={total} />
    <QuestionCard
      question={question}
      selectedOptionId={selectedOptionId}
      onSelect={onSelect}
      disabled={answered}
      reveal={answered && mode === 'estudo'}
    />

    <div className="actions">
      {!answered ? (
        <button type="button" className="main-btn" onClick={onConfirm} disabled={!selectedOptionId}>
          Confirmar resposta
        </button>
      ) : (
        <button type="button" className="main-btn" onClick={onNext}>
          {index + 1 >= total ? 'Finalizar prova' : 'Próxima questão'}
        </button>
      )}
    </div>

    {answered && selectedOptionId && (
      <FeedbackPanel question={question} selectedOptionId={selectedOptionId} mode={mode} />
    )}
  </section>
);
