from abc import ABC, abstractmethod

class IFileHandlerService(ABC):
    @abstractmethod
    def fetch_and_filter_files(self, directory: str, condition):
        """Fetch and return files from the specified directory that satisfy the given condition."""
        pass