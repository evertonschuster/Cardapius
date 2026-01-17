from postprocess.temporal_smoother import TemporalSmoother


def test_temporal_smoother_averages_window() -> None:
    smoother = TemporalSmoother(window_secs=10)
    first = smoother.push("cam1:violence", 0.2, 1)
    second = smoother.push("cam1:violence", 0.6, 1)

    assert round(first, 2) == 0.2
    assert round(second, 2) == 0.4
