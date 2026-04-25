import React, { useMemo, useState } from 'react';
import { questions as allQuestions } from '../data/questions';
import { OptionId, Question, QuizMode, UserAnswer } from '../types';
import { StartScreen } from '../components/StartScreen';
import { QuizScreen } from '../components/QuizScreen';
import { ResultScreen } from '../components/ResultScreen';
import { ReviewScreen } from '../components/ReviewScreen';
import '../styles.css';

type Stage = 'start' | 'quiz' | 'result' | 'review';
const BEST_SCORE_KEY = 'network_quiz_best_score';

const hasNoTripleRepeat = (items: Question[]) => {
  for (let i = 2; i < items.length; i += 1) {
    if (
      items[i].correctOptionId === items[i - 1].correctOptionId &&
      items[i].correctOptionId === items[i - 2].correctOptionId
    ) {
      return false;
    }
  }
  return true;
};

const shuffleQuestions = (items: Question[]) => {
  const clone = [...items];
  for (let i = clone.length - 1; i > 0; i -= 1) {
    const j = Math.floor(Math.random() * (i + 1));
    [clone[i], clone[j]] = [clone[j], clone[i]];
  }
  return clone;
};

const getQuestionSet = () => {
  for (let attempt = 0; attempt < 300; attempt += 1) {
    const candidate = shuffleQuestions(allQuestions);
    if (hasNoTripleRepeat(candidate)) return candidate;
  }
  return allQuestions;
};

export const Home = () => {
  const [stage, setStage] = useState<Stage>('start');
  const [mode, setMode] = useState<QuizMode>('estudo');
  const [currentIndex, setCurrentIndex] = useState(0);
  const [selectedOptionId, setSelectedOptionId] = useState<OptionId | null>(null);
  const [answered, setAnswered] = useState(false);
  const [answers, setAnswers] = useState<UserAnswer[]>([]);
  const [questionSet, setQuestionSet] = useState<Question[]>(allQuestions);
  const [bestScore, setBestScore] = useState<number>(() => Number(localStorage.getItem(BEST_SCORE_KEY) ?? 0));

  const currentQuestion = questionSet[currentIndex];
  const score = useMemo(() => answers.filter((a) => a.isCorrect).length, [answers]);

  const startQuiz = () => {
    setQuestionSet(getQuestionSet());
    setStage('quiz');
    setCurrentIndex(0);
    setSelectedOptionId(null);
    setAnswered(false);
    setAnswers([]);
  };

  const confirmAnswer = () => {
    if (!selectedOptionId || !currentQuestion) return;
    const isCorrect = selectedOptionId === currentQuestion.correctOptionId;

    setAnswers((prev) => [
      ...prev,
      { questionId: currentQuestion.id, selectedOptionId, isCorrect }
    ]);
    setAnswered(true);
  };

  const goToNext = () => {
    if (currentIndex + 1 >= questionSet.length) {
      const finalScore = answers.filter((a) => a.isCorrect).length;
      if (finalScore > bestScore) {
        localStorage.setItem(BEST_SCORE_KEY, String(finalScore));
        setBestScore(finalScore);
      }
      setStage('result');
      return;
    }

    setCurrentIndex((prev) => prev + 1);
    setSelectedOptionId(null);
    setAnswered(false);
  };

  if (stage === 'start') {
    return (
      <main className="quiz-root">
        <StartScreen bestScore={bestScore} selectedMode={mode} onModeChange={setMode} onStart={startQuiz} />
      </main>
    );
  }

  if (stage === 'quiz' && currentQuestion) {
    return (
      <main className="quiz-root">
        <QuizScreen
          mode={mode}
          question={currentQuestion}
          index={currentIndex}
          total={questionSet.length}
          selectedOptionId={selectedOptionId}
          answered={answered}
          onSelect={setSelectedOptionId}
          onConfirm={confirmAnswer}
          onNext={goToNext}
        />
      </main>
    );
  }

  if (stage === 'result') {
    return (
      <main className="quiz-root">
        <ResultScreen
          score={score}
          total={questionSet.length}
          bestScore={bestScore}
          mode={mode}
          onReview={() => setStage('review')}
          onRetry={startQuiz}
        />
      </main>
    );
  }

  return (
    <main className="quiz-root">
      <ReviewScreen questions={questionSet} answers={answers} onRestart={() => setStage('start')} />
    </main>
  );
};
